import React, { useState, useEffect } from "react";
import Search from "./Search";
import DeckList from "./DeckList";
import LeftNav, { type NavItem }  from "./LeftNav";
import Summary from "./Summary";

const STORAGE_KEY = "edhwreck.navItems.v1";

const makeId = () => Date.now().toString(36) + Math.random().toString(36).slice(2, 8);

const Home = () => {
    const [cardName, setCardName] = useState<string | undefined>(undefined);
    const [navItems, setNavItems] = useState<NavItem[]>([]);
    const [activeId, setActiveId] = useState<string | undefined>(undefined);
    const [summaryCount, setSummaryCount] = useState(0);

    useEffect(() => {
        try {
            const json = localStorage.getItem(STORAGE_KEY);
            if (json) {
                const parsed = JSON.parse(json);
                if (Array.isArray(parsed)) {
                    // migration: if parsed array of strings => convert to NavItem[]
                    if (parsed.length === 0 || typeof parsed[0] === "string") {
                        const items = (parsed as string[]).map((s) => ({
                            id: makeId(),
                            kind: "search" as const,
                            title: s,
                            query: s,
                        }));
                        setNavItems(items);
                    } else {
                        setNavItems(parsed as NavItem[]);
                        // compute summaryCount from existing summaries
                        const count = (parsed as NavItem[]).filter((it) => it.kind === "summary").length;
                        setSummaryCount(count);
                    }
                }
            }
        } catch {
            // ignore parse errors
        }
    }, []);

    useEffect(() => {
        try {
            localStorage.setItem(STORAGE_KEY, JSON.stringify(navItems));
        } catch {
            // ignore storage errors
        }
    }, [navItems]);

    const addOrBringToFrontSearch = (searchText: string) => {
        const trimmed = searchText.trim();
        if (!trimmed) return;

        setNavItems((prev) => {
            const idx = prev.findIndex((it) => it.kind === "search" && (it.query ?? "").trim().toLowerCase() === trimmed.toLowerCase());
            const next = prev.slice();
            if (idx !== -1) next.splice(idx, 1);
            next.unshift({ id: makeId(), kind: "search", title: trimmed, query: trimmed });
            return next;
        });
    };

    const handleSearch = (value: string) => {
        const trimmed = value.trim();
        if (!trimmed) {
            setCardName(undefined);
            setActiveId(undefined);
            return;
        }

        setCardName(trimmed);
        setActiveId(undefined); // clear active nav selection (we're using free search entry)

        // persist saved search as nav item and bring to front
        addOrBringToFrontSearch(trimmed);
    };

    const handleSelectNav = (id: string) => {
        const item = navItems.find((it) => it.id === id);
        if (!item) {
            setActiveId(undefined);
            setCardName(undefined);
            return;
        }

        setActiveId(id);
        if (item.kind === "search") {
            setCardName(item.query);
        } else {
            // summary selected: clear card search input and results
            setCardName(undefined);
        }
    };

    const handleRemoveNav = (id: string) => {
        setNavItems((prev) => prev.filter((it) => it.id !== id));
        setActiveId((current) => (current === id ? undefined : current));
        setCardName((current) => {
            const removed = navItems.find((it) => it.id === id);
            if (!removed) return current;
            if (removed.kind === "search" && current && removed.query && current.trim().toLowerCase() === removed.query.trim().toLowerCase()) {
                return undefined;
            }
            return current;
        });
    };

    const handleBulkRemove = (ids: string[]) => {
        if (!Array.isArray(ids) || ids.length === 0) return;
        setNavItems((prev) => prev.filter((it) => !ids.includes(it.id)));
        setActiveId((current) => (current && ids.includes(current) ? undefined : current));
        setCardName((current) => {
            // if removed search equals current cardName, clear
            const removedSearches = navItems.filter((it) => ids.includes(it.id) && it.kind === "search").map((it) => it.query);
            if (!current) return current;
            if (removedSearches.some((r) => r && r.trim().toLowerCase() === current.trim().toLowerCase())) return undefined;
            return current;
        });
    };

    const handleBulkSummarize = (ids: string[]) => {
        if (!Array.isArray(ids) || ids.length === 0) return;
        // gather selected search queries (ignore non-search items)
        const selectedSearches = navItems.filter((it) => ids.includes(it.id) && it.kind === "search").map((it) => it.query ?? "").filter(Boolean);
        if (selectedSearches.length === 0) return;

        const nextIdx = summaryCount + 1;
        const title = `Summary ${nextIdx}`;
        const summaryItem: NavItem = {
            id: makeId(),
            kind: "summary",
            title,
            searches: selectedSearches,
        };

        setNavItems((prev) => [summaryItem, ...prev]);
        setSummaryCount(nextIdx);
        setActiveId(summaryItem.id);
        setCardName(undefined);
    };

    const findNavIdByQuery = (query: string) => {
        const found = navItems.find((it) => it.kind === "search" && (it.query ?? "").trim().toLowerCase() === query.trim().toLowerCase());
        return found?.id;
    };

    const existsSearch = (query: string) => !!findNavIdByQuery(query);

    const openSearchFromSummary = (query: string) => {
        const id = findNavIdByQuery(query);
        if (id) handleSelectNav(id);
    };

    return (
        <>
            <h1>EdhWreck Home</h1>

            <div className="container">
                <div className="row">
                    <div className="col-12 col-md-3 mb-3">
                        <LeftNav
                            items={navItems}
                            activeId={activeId}
                            onSelect={handleSelectNav}
                            onRemove={handleRemoveNav}
                            onBulkRemove={handleBulkRemove}
                            onBulkSummarize={handleBulkSummarize}
                        />
                    </div>

                    <div className="col-12 col-md-9">
                        {activeId ? (
                            (() => {
                                const active = navItems.find((it) => it.id === activeId);
                                if (!active) return (
                                    <>
                                        <Search placeholder="Pick a card, any card..." onClick={handleSearch} value={cardName ?? ""} />
                                        <div className="mt-3"><DeckList cardName={cardName} /></div>
                                    </>
                                );

                                if (active.kind === "summary") {
                                    return (
                                        <div>
                                            <Summary
                                                title={active.title}
                                                searches={active.searches ?? []}
                                                onOpenSearch={openSearchFromSummary}
                                                existsSearch={existsSearch}
                                            />
                                        </div>
                                    );
                                }

                                // active is a search
                                return (
                                    <>
                                        <Search placeholder="Pick a card, any card..." onClick={handleSearch} value={cardName ?? (active.query ?? "")} />
                                        <div className="mt-3"><DeckList cardName={active.query} /></div>
                                    </>
                                );
                            })()
                        ) : (
                            <>
                                <Search placeholder="Pick a card, any card..." onClick={handleSearch} value={cardName ?? ""} />
                                <div className="mt-3">
                                    <DeckList cardName={cardName} />
                                </div>
                            </>
                        )}
                    </div>
                </div>
            </div>
        </>
    );
};

export default Home;