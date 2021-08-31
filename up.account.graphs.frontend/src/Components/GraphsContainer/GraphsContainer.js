import PieGraph from "../Graphs/PieGraph";

const GraphsContainer = ({ transactionData }) => (
  <div className="row justify-content-center Text-align-center mt-4">
    GRAPHS
    <br></br>
    <PieGraph transactionData={transactionData} />
  </div>
);

export default GraphsContainer;
