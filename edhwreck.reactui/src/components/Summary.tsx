import React, { useEffect, useState } from "react";

interface SummaryProps {
    title: string;
    searches: string[]; // queries used to build the summary
    // callback to open a saved search by query (Home will map to nav item if present)
    onOpenSearch: (search: string) => void;
    // function to check if a saved search still exists
    existsSearch: (search: string) => boolean;
}

type AggregatedCard = {
    id: string;
    name: string;
    set_name?: string;
    set?: string; // set code / abbreviation
    smallImage?: string;
    normalImage?: string;
    count: number; // number of searches this card appeared in
    percent: number; // derived
};

const getSmallImageUrl = (card: any): string | undefined => {
    if (card?.image_uris?.small) return card.image_uris.small;
    if (Array.isArray(card?.card_faces)) {
        for (const face of card.card_faces) {
            if (face?.image_uris?.small) return face.image_uris.small;
        }
    }
    if (card?.image_uris?.normal) return card.image_uris.normal;
    return undefined;
};

const getNormalImageUrl = (card: any): string | undefined => {
    if (card?.image_uris?.normal) return card.image_uris.normal;

    if (Array.isArray(card?.card_faces)) {
        for (const face of card.card_faces) {
            if (face?.image_uris?.normal) return face.image_uris.normal;
        }
    }

    // fallbacks
    if (card?.image_uris?.large) return card.image_uris.large;
    if (Array.isArray(card?.card_faces) && card.card_faces[0]?.image_uris?.large)
        return card.card_faces[0].image_uris.large;

    return undefined;
};

const Summary = ({ title, searches, onOpenSearch, existsSearch }: SummaryProps) => {
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<string | null>(null);
    const [cards, setCards] = useState<AggregatedCard[]>([]);
    const [copied, setCopied] = useState(false);
    const [copiedNote, setCopiedNote] = useState<string | null>(null);
    const [hoveredId, setHoveredId] = useState<string | null>(null);

    useEffect(() => {
        let mounted = true;
        const fetchAndAggregate = async () => {
            setLoading(true);
            setError(null);
            try {
                // fetch each search in parallel
                const fetches = searches.map(async (q) => {
                    try {
                        const resp = await fetch(`https://api.scryfall.com/cards/search?q=${encodeURIComponent(q)}&order=edhrec&game=paper`, {
                            headers: { "User-Agent": "edhwreck.reactui/1.0", Accept: "application/json" },
                        });
                        if (!resp.ok) return { q, cards: [] as any[] };
                        const json = await resp.json();
                        return { q, cards: Array.isArray(json?.data) ? json.data : [] };
                    } catch {
                        return { q, cards: [] as any[] };
                    }
                });

                const results = await Promise.all(fetches);

                // Map of cardId -> { card info, count of searches it appeared in }
                const map = new Map<string, { card: any; count: number }>();

                // For each search result, build a set of card ids to avoid double counting within same search
                for (const res of results) {
                    const seen = new Set<string>();
                    for (const c of res.cards) {
                        const id = c?.id ?? `${c?.name}::${c?.set}`; // fallback key
                        if (!id) continue;
                        if (seen.has(id)) continue;
                        seen.add(id);

                        const entry = map.get(id);
                        if (!entry) map.set(id, { card: c, count: 1 });
                        else entry.count += 1;
                    }
                }

                const totalSearches = Math.max(1, searches.length);

                const aggregated: AggregatedCard[] = Array.from(map.entries()).map(([id, v]) => ({
                    id,
                    name: v.card?.name ?? id,
                    set_name: v.card?.set_name ?? v.card?.set,
                    set: v.card?.set,
                    smallImage: getSmallImageUrl(v.card),
                    normalImage: getNormalImageUrl(v.card),
                    count: v.count,
                    percent: Math.round((v.count / totalSearches) * 1000) / 10, // one decimal
                }));

                // sort by count desc then name
                aggregated.sort((a, b) => {
                    if (b.count !== a.count) return b.count - a.count;
                    return a.name.localeCompare(b.name);
                });

                const top = aggregated.slice(0, 100);

                if (mounted) setCards(top);
            } catch (err: any) {
                if (mounted) setError(String(err));
            } finally {
                if (mounted) setLoading(false);
            }
        };

        fetchAndAggregate();
        return () => {
            mounted = false;
        };
    }, [searches]);

    const buildCopyText = (cardsToCopy: AggregatedCard[]): string => {
        // Format: "- {card name} ({set abbreviation})"
        return cardsToCopy
            .map((c) => {
                const name = c?.name ?? "";
                const setAbbr = c?.set ? String(c.set).toUpperCase() : (c?.set_name ? String(c.set_name) : "");
                //return `- ${name} (${setAbbr})`;
                return `1 ${name}`;
            })
            .join("\n");
    };

    const copyToClipboard = async (cardsToCopy: AggregatedCard[]) => {
        if (!navigator?.clipboard) {
            console.warn("Clipboard API not available");
            return;
        }

        // Only copy top 100 if list is larger than 100
        const limit = 100;
        const willLimit = cardsToCopy.length > limit;
        const listToCopy = willLimit ? cardsToCopy.slice(0, limit) : cardsToCopy;
        const text = buildCopyText(listToCopy);

        try {
            await navigator.clipboard.writeText(text);
            setCopied(true);
            setCopiedNote(willLimit ? `Copied top ${limit}` : "Copied");
            window.setTimeout(() => {
                setCopied(false);
                setCopiedNote(null);
            }, 2000);
        } catch (err) {
            console.error("Failed to copy to clipboard:", err);
        }
    };

    return (
        <div>
            <div className="d-flex align-items-center justify-content-between mb-3">
                <h2 className="mb-0">Summary: {title}</h2>
                <div className="d-flex align-items-center">
                    <small className="text-muted me-3">Based on {searches.length} searches</small>
                    <button
                        type="button"
                        className="btn btn-outline-secondary btn-sm"
                        onClick={() => copyToClipboard(cards)}
                        aria-live="polite"
                    >
                        Copy list
                    </button>
                    {cards.length > 100 && <small className="text-muted ms-2">Top 100 will be copied</small>}
                    {copied && copiedNote && <span className="ms-2 text-success small">{copiedNote}</span>}
                </div>
            </div>

            <div className="mb-3">
                <strong>Included searches:</strong>
                <ul className="list-inline mt-2">
                    {searches.map((s) => {
                        const exists = existsSearch(s);
                        return (
                            <li key={s} className="list-inline-item me-2">
                                {exists ? (
                                    <button type="button" className="btn btn-link p-0" onClick={() => onOpenSearch(s)}>
                                        {s}
                                    </button>
                                ) : (
                                    <span className="text-muted small">{s} (deleted)</span>
                                )}
                            </li>
                        );
                    })}
                </ul>
            </div>

            {loading && <div>Loading summary...</div>}
            {error && <div className="text-danger">Error: {error}</div>}

            {!loading && !error && (
                <>
                    <div className="mb-2">
                        <small className="text-muted">Top {cards.length} cards by frequency</small>
                    </div>

                    <div className="row g-3">
                        {cards.map((c) => (
                            <div key={c.id} className="col-6 col-sm-4 col-md-3 col-lg-2">
                                <div
                                    className="card h-100 shadow-sm"
                                    onMouseEnter={() => setHoveredId(c.id)}
                                    onMouseLeave={() => setHoveredId(null)}
                                >
                                    {c.smallImage ? (
                                        <img src={c.smallImage} alt={c.name} className="card-img-top" />
                                    ) : (
                                        <div className="d-flex align-items-center justify-content-center bg-light" style={{ height: 200 }}>
                                            <span className="text-muted small">No image</span>
                                        </div>
                                    )}

                                    {/* Hover preview tooltip: shows the "normal" image */}
                                    {hoveredId === c.id && c.normalImage && (
                                        <div
                                            className="position-absolute"
                                            style={{
                                                zIndex: 2000,
                                                top: -8,
                                                left: "100%",
                                                transform: "translateX(8px)",
                                                width: 320,
                                                maxWidth: "50vw",
                                                borderRadius: 6,
                                                overflow: "hidden",
                                                boxShadow: "0 6px 18px rgba(0,0,0,0.25)",
                                                background: "#fff",
                                            }}
                                        >
                                            <img
                                                src={c.normalImage}
                                                alt={`${c.name} (preview)`}
                                                style={{ display: "block", width: "100%", height: "auto" }}
                                            />
                                        </div>
                                    )}

                                    <div className="card-body p-2">
                                        <div className="d-flex align-items-center justify-content-between">
                                            <h6 className="card-title mb-1" title={c.name} style={{ fontSize: "0.85rem" }}>
                                                {c.name}
                                            </h6>
                                            <span className="badge bg-primary">{c.percent}%</span>
                                        </div>
                                        {c.set_name && <p className="card-text small text-muted mb-0">{c.set_name}</p>}
                                    </div>
                                </div>
                            </div>
                        ))}
                    </div>
                </>
            )}
        </div>
    );
};

export default Summary;