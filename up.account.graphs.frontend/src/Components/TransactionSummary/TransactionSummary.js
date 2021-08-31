import { useState } from "react";

const TransactionSummary = ({ transactionData }) => {
  console.log(transactionData);
  return (
    <div
      className="row justify-content-around Text-align-center"
      style={{ width: "100%" }}
    >
      <div className="col-sm-4 container-border">
        <h4 className="mt-2 mb-2 bold-text text-decoration-underlined">
          OUTGOING TRANSACTIONS - ${transactionData.totalOutflow.toFixed(2)}
        </h4>
        {Object.keys(transactionData.outgoingTransactions).map((key, index) => (
          <div className="mt-2 mb-2" key={index}>
            <span className="bold-text mr-2">{key}</span> $
            {transactionData.outgoingTransactions[key].toFixed(2)}
          </div>
        ))}
      </div>
      <div className="col-sm-4 container-border">
        <h4 className="mt-2 mb-2 bold-text text-decoration-underlined">
          INCOMING TRANSACTIONS - ${transactionData.totalInflow.toFixed(2)}
        </h4>
        {Object.keys(transactionData.incomingTransactions).map((key, index) => (
          <div className="mt-2 mb-2" key={index}>
            <span className="bold-text mr-2">{key}</span> $
            {transactionData.incomingTransactions[key].toFixed(2)}
          </div>
        ))}
      </div>
    </div>
  );
};

export default TransactionSummary;
