import React, { useState, useEffect } from "react";

export type NavItem = {
    id: string;
    kind: "search" | "summary";
    title: string;
    query?: string; // present for kind === 'search'
    searches?: string[]; // present for kind === 'summary'
};

interface LeftNavProps {
    items: NavItem[];
    activeId?: string | undefined;
    onSelect: (id: string) => void;
    onRemove: (id: string) => void;
    onBulkRemove?: (ids: string[]) => void;
    onBulkSummarize?: (ids: string[]) => void;
}

const LeftNav = ({ items, activeId, onSelect, onRemove, onBulkRemove, onBulkSummarize }: LeftNavProps) => {
    const [selected, setSelected] = useState<Record<string, boolean>>({});
    const [selectAll, setSelectAll] = useState(false);

    useEffect(() => {
        // keep selection keys in sync when items change
        const next: Record<string, boolean> = {};
        items.forEach((it) => {
            next[it.id] = !!selected[it.id];
        });
        setSelected(next);
        setSelectAll(items.length > 0 && items.every((it) => next[it.id]));
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [items]);

    const toggleItem = (id: string) => {
        setSelected((prev) => {
            const next = { ...prev, [id]: !prev[id] };
            setSelectAll(items.length > 0 && items.every((it) => next[it.id]));
            return next;
        });
    };

    const toggleSelectAll = () => {
        const nextAll = !selectAll;
        const next: Record<string, boolean> = {};
        items.forEach((it) => (next[it.id] = nextAll));
        setSelected(next);
        setSelectAll(nextAll);
    };

    const getSelectedIds = () => items.filter((it) => !!selected[it.id]).map((it) => it.id);

    const handleBulkDelete = () => {
        const ids = getSelectedIds();
        if (ids.length === 0) return;
        if (onBulkRemove) onBulkRemove(ids);
        else ids.forEach((id) => onRemove(id));
        // reset selection
        const cleared: Record<string, boolean> = {};
        items.forEach((it) => (cleared[it.id] = false));
        setSelected(cleared);
        setSelectAll(false);
    };

    const handleBulkSummarize = () => {
        const ids = getSelectedIds();
        if (ids.length === 0) return;
        if (onBulkSummarize) onBulkSummarize(ids);
        // do not clear selection to allow further actions if desired
    };

    return (
        <aside>
            <div className="card">
                <div className="card-header d-flex align-items-center justify-content-between">
                    <strong>Saved</strong>

                    <div className="d-flex align-items-center">
                        <div className="form-check form-check-inline me-2">
                            <input
                                id="leftnav-select-all"
                                className="form-check-input"
                                type="checkbox"
                                checked={selectAll}
                                onChange={toggleSelectAll}
                                aria-label="Select all"
                            />
                        </div>

                        <button
                            type="button"
                            className="btn btn-sm btn-danger me-2"
                            onClick={handleBulkDelete}
                            disabled={getSelectedIds().length === 0}
                        >
                            Delete selected
                        </button>

                        <button
                            type="button"
                            className="btn btn-sm btn-secondary"
                            onClick={handleBulkSummarize}
                            disabled={getSelectedIds().length === 0}
                        >
                            Summarize selected
                        </button>
                    </div>
                </div>

                <div className="list-group list-group-flush">
                    {items.length === 0 && <div className="list-group-item text-muted">No items</div>}

                    {items.map((it) => {
                        const isActive = activeId === it.id;
                        const isChecked = !!selected[it.id];

                        return (
                            <div
                                key={it.id}
                                className={`list-group-item d-flex justify-content-between align-items-center ${isActive ? "active" : ""}`}
                            >
                                <div className="d-flex align-items-center flex-grow-1">
                                    <input
                                        className="form-check-input me-2"
                                        type="checkbox"
                                        checked={isChecked}
                                        onChange={() => toggleItem(it.id)}
                                        aria-label={`Select ${it.title}`}
                                    />

                                    <button
                                        type="button"
                                        onClick={() => onSelect(it.id)}
                                        className={`btn btn-link p-0 text-start flex-grow-1 ${isActive ? "text-white" : ""}`}
                                        style={{ textDecoration: "none" }}
                                    >
                                        {it.title}
                                    </button>
                                </div>

                                <button
                                    type="button"
                                    aria-label={`Remove ${it.title}`}
                                    className="btn btn-sm btn-outline-secondary ms-2"
                                    onClick={() => onRemove(it.id)}
                                >
                                    ×
                                </button>
                            </div>
                        );
                    })}
                </div>
            </div>
        </aside>
    );
};

export default LeftNav;