import "./App.css";
import { RouterProvider } from "react-router-dom";

import { router } from "./constants/router";

function App() {
    return (
        <div>
            <RouterProvider router={router} />
        </div>
    );
}

export default App;
