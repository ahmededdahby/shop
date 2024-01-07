import logo from "./logo.svg";
import "./App.css";
import ProductList from "./Components/ProductList/ProductList.tsx";
import { PrimeReactProvider } from "primereact/api";
import { Button } from "primereact/button";
import 'bootstrap/dist/css/bootstrap.min.css';
function App() {
  return (
    <PrimeReactProvider>
      <ProductList />
    </PrimeReactProvider>
  );
}

export default App;
