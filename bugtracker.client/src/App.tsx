import './App.css';

function App() {


    const contents = <p>
        <em>Loading... Please refresh once the ASP.NET backend has started. See
            <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.
        </em>
    </p>
    return (
        <div>
            <h1 id="tabelLabel">Weather forecast</h1>
            <p>This component demonstrates fetching data from the server.</p>
            {contents}
        </div>
    );
}

export default App;