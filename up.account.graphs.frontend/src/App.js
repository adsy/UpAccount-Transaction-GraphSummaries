import logo from "./logo.svg";
import "./App.css";
import Header from "./Components/AppHeader/Header";
import TransactionSummary from "./Components/TransactionSummary/TransactionSummary";
import "bootstrap/dist/css/bootstrap.min.css";
import { useState, useEffect } from "react";
import { Constants } from "./Constants/Constants";
import GraphsContainer from "./Components/GraphsContainer/GraphsContainer";

function App() {
  const [loading, setLoading] = useState(true);
  const [transactionData, setTransactionData] = useState({});

  useEffect(async () => {
    // console.log(Constants.getTransactionData);
    await fetch(
      "https://localhost:44326/api/UpAccount/GetTransactions/2021-06-06/2021-08-20",
      {
        method: "GET", // *GET, POST, PUT, DELETE, etc.
        headers: {
          "Content-Type": "application/json",
        },
      }
    )
      .then((response) => response.json())
      .then((data) => {
        setTransactionData(data);
        setLoading(false);
      })
      .catch((error) => console.error(error));
  }, []);

  if (loading) {
    return (
      <div>
        <Header />
        <div className="screen-center loading">LOADING</div>
      </div>
    );
  }

  return (
    <div>
      <Header />
      <TransactionSummary transactionData={transactionData} />
      <GraphsContainer transactionData={transactionData} />
    </div>
  );
}

export default App;
