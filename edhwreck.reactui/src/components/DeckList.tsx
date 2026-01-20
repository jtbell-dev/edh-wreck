import React, { useState, useEffect } from "react";

interface DeckListProps {
    cardName?: string;
}

const DeckList = ({ cardName }: DeckListProps) => {
    const [data, setData] = useState<any | null>(null);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState<any | null>(null);
    const [hoveredId, setHoveredId] = useState<string | null>(null);
    const [copied, setCopied] = useState(false);
    const [copiedNote, setCopiedNote] = useState<string | null>(null);

    useEffect(() => {
        // If no cardName, clear previous results and do nothing
        if (!cardName) {
            setData(null);
            setError(null);
            setLoading(false);
            return;
        }

        const fetchData = async () => {
            setLoading(true);
            setError(null);
            try {
                const q = encodeURIComponent(cardName);
                const response = await fetch(
                    `https://api.scryfall.com/cards/search?q=${q}&order=edhrec&game=paper`,
                    {
                        headers: {
                            "User-Agent": "edhwreck.reactui/1.0",
                            Accept: "application/json",
                        },
                    }
                );

                if (!response.ok) {
                    throw new Error(`HTTP error! status: ${response.status}`);
                }

                const jsonData = await response.json();
                setData(jsonData);
            } catch (err) {
                console.error("Error fetching data:", err);
                setData(null);
                setError(err);
            } finally {
                setLoading(false);
            }
        };

        fetchData();
    }, [cardName]);

    const getImageUrl = (card: any): string => {
        // Scryfall standard: card.image_uris is an object with keys like "small", "normal"
        if (card?.image_uris?.small) return card.image_uris.small;

        // Double-faced cards: image_uris often live on card_faces
        if (Array.isArray(card?.card_faces)) {
            for (const face of card.card_faces) {
                if (face?.image_uris?.small) return face.image_uris.small;
            }
        }

        // Fallbacks to other sizes if small not available
        if (card?.image_uris?.normal) return card.image_uris.normal;
        if (Array.isArray(card?.card_faces) && card.card_faces[0]?.image_uris?.normal)
            return card.card_faces[0].image_uris.normal;

        return "";
    };

    const getNormalUrl = (card: any): string => {
        if (card?.image_uris?.normal) return card.image_uris.normal;

        if (Array.isArray(card?.card_faces)) {
            for (const face of card.card_faces) {
                if (face?.image_uris?.normal) return face.image_uris.normal;
            }
        }

        // Fallback to other sizes if normal not available
        if (card?.image_uris?.large) return card.image_uris.large;
        if (Array.isArray(card?.card_faces) && card.card_faces[0]?.image_uris?.large)
            return card.card_faces[0].image_uris.large;

        return "";
    };

    const buildCopyText = (cardsToCopy: any[]): string => {
        // Format: "- {card name} ({set abbreviation})"
        return cardsToCopy
            .map((c) => {
                const name = c?.name ?? "";
                const setAbbr = c?.set ? String(c.set).toUpperCase() : "";
                return `- ${name} (${setAbbr})`;
            })
            .join("\n");
    };

    const copyToClipboard = async (cards: any[]) => {
        if (!navigator?.clipboard) {
            console.warn("Clipboard API not available");
            return;
        }

        // Only copy top 100 if list is larger than 100
        const limit = 100;
        const willLimit = cards.length > limit;
        const listToCopy = willLimit ? cards.slice(0, limit) : cards;
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

    if (!cardName) return <div>Enter a card name and click Search.</div>;
    if (loading) return <div>Loading...</div>;
    if (error) return <div>Error: {String(error)}</div>;

    const cards: any[] = Array.isArray(data?.data) ? data.data : [];
    const totalCount: number = typeof data?.total_cards === "number" ? data.total_cards : cards.length;

    if (cards.length === 0) return <div>No results.</div>;

    return (
        <div className="container">
            <div className="d-flex align-items-center mb-3">
                <h2 className="mb-0">Search results</h2>

                <div className="ms-3 d-flex align-items-center">
                    <small className="text-muted me-3">Total: {totalCount}</small>

                    <button
                        type="button"
                        className="btn btn-outline-secondary btn-sm"
                        onClick={() => copyToClipboard(cards)}
                        aria-live="polite"
                    >
                        Copy list
                    </button>

                    {cards.length > 100 && (
                        <small className="text-muted ms-2">Top 100 will be copied</small>
                    )}

                    {copied && copiedNote && <span className="ms-2 text-success small">{copiedNote}</span>}
                </div>
            </div>

            {/* Responsive Bootstrap grid of cards */}
            <div className="row g-3">
                {cards.map((card) => {
                    const smallImg = getImageUrl(card);
                    const normalImg = getNormalUrl(card);
                    const id = card.id ?? card.collector_number ?? "";

                    return (
                        <div
                            key={id || Math.random().toString(36).slice(2)}
                            className="col-6 col-sm-4 col-md-3 col-lg-2"
                        >
                            <div
                                className="card h-100 shadow-sm"
                                onMouseEnter={() => setHoveredId(id || null)}
                                onMouseLeave={() => setHoveredId(null)}
                            >
                                <div className="position-relative">
                                    {smallImg ? (
                                        <img
                                            src={smallImg}
                                            className="card-img-top"
                                            alt={card.name}
                                            // aria-describedby or title can help screen readers
                                            title={card.name}
                                        />
                                    ) : (
                                        <div
                                            className="d-flex align-items-center justify-content-center bg-light"
                                            style={{ height: 200 }}
                                        >
                                            <span className="text-muted small">No image</span>
                                        </div>
                                    )}

                                    {/* Hover preview tooltip: shows the "normal" image */}
                                    {hoveredId === id && normalImg && (
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
                                                src={normalImg}
                                                alt={`${card.name} (preview)`}
                                                style={{ display: "block", width: "100%", height: "auto" }}
                                            />
                                        </div>
                                    )}
                                </div>

                                <div className="card-body p-2">
                                    <h6 className="card-title mb-1" title={card.name}>
                                        {card.name}
                                    </h6>
                                    {card.set_name && <p className="card-text small text-muted mb-0">{card.set_name}</p>}
                                </div>
                            </div>
                        </div>
                    );
                })}
            </div>
        </div>
    );
};

export default DeckList;