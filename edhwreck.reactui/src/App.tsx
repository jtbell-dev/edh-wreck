//import Message from './tutorial-components/Message';
//import ListGroup from './tutorial-components/ListGroup';
import Alert from './tutorial-components/Alert';
import Button from './tutorial-components/Button';
import { useState } from "react";

function App() {

    //  let items = [
    //     'New York',
    //     'San Francisco',
    //     'London',
    //     'Paris',
    //     'Tokyo'
    // ];
    // 
    // const handleSelectItem = (item: string) => { console.log(item); }
    // 
    // return <div><ListGroup items={items} heading="Cities" onSelectItem={handleSelectItem}/></div>;

    let [showAlert, setShowAlert] = useState(false);

    return (
        <div>
            { showAlert && <Alert onClose={ () => setShowAlert(false) }>Hello <b>World</b>!</Alert> }
            <Button onClick={ () => setShowAlert(true) }>My Button</Button>
        </div>
    );
}

export default App;