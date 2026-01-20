import React, { useState, useEffect } from "react";
interface SearchProps {
    placeholder: string;
    onClick: (value: string) => void;
    value?: string;
}

const Search = ({ placeholder, onClick, value }: SearchProps) => {
    const [valueState, setValueState] = useState("");

    useEffect(() => {
        // sync from parent when `value` changes (e.g. selecting a saved search)
        setValueState(value ?? "");
    }, [value]);

    const handleKeyDown = (e: React.KeyboardEvent<HTMLInputElement>) => {
        if (e.key === "Enter") {
            onClick(valueState);
        }
    };

    return (
        <>
            <input
                type="text"
                className="form-control"
                placeholder={placeholder}
                value={valueState}
                onChange={(e) => setValueState(e.target.value)}
                onKeyDown={handleKeyDown}
            />
            <button
                className="btn btn-primary mt-2"
                onClick={() => onClick(valueState)}
            >
                Search
            </button>
        </>
    );
};

export default Search;