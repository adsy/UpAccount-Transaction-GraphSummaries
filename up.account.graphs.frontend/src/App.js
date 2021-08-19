import logo from "./logo.svg";
import "./App.css";
import Header from "./Components/AppHeader/Header";
import TransactionSummary from "./Components/TransactionSummary/TransactionSummary";
import "bootstrap/dist/css/bootstrap.min.css";

function App() {
  return (
    <div>
      <Header />
      <TransactionSummary />
    </div>
  );
}

export default App;
