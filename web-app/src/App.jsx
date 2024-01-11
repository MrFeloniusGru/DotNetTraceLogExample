import React from "react";
import { context, trace, SpanStatusCode } from "@opentelemetry/api";

const App = () => {
  return (
    <React.Fragment>
      <h1>Hello React</h1>
      <ui>
        <li>
          <button onClick={() => {}}>Path A</button>
          <button>Path B</button>
        </li>
      </ui>
    </React.Fragment>
  );
};

export default App;