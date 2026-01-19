function Message() {
    // describe what the UI is going to look like when using this component
    // JSX: JavaScript XML
    const name = '';
    if (name) {
        return <h1>Hello {name}</h1>;
    }
    else {
        return <h1>Hello World</h1>;
    }
    
}

export default Message;