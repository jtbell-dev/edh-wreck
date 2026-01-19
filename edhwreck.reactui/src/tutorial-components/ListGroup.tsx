import { MouseEvent, useState } from "react";

interface ListGroupProps {
    items: string[];
    heading: string;
    onSelectItem: (item: string) => void;
}

function ListGroup({items, heading, onSelectItem}: ListGroupProps) {

    const [selectedIndex, setSelectedIndex] = useState(-1);
    // arr[0] // variable (selectedIndex)
    // arr[1] // function to update the variable
    

   

    //items = []; // Uncomment this line to test the "no items" case

    //const getMessage = () => {
    //    return items.length === 0 ? <p>No items found</p> : null;
    //}

    // Event Handler
    //const handleClick = (index: number) => { selectedIndex = index; };

    // <> and </> are Fragment shorthand tags
    return (
        <> 
          <h1>{ heading }</h1>
          { items.length === 0 && <p>No items found</p> }
          <ul className="list-group">
            {items.map((item, index) => (
                <li key={item} 
                    onClick={ () => {
                        setSelectedIndex(index);
                        onSelectItem(item);
                        }
                    }
                    className={selectedIndex === index ? "list-group-item active" : "list-group-item" }>
                    {item}
                </li>
            ))}
          </ul>
        </>
    );
}

export default ListGroup;