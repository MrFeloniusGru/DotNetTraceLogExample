import React from "react";
import { context, trace, SpanStatusCode } from "@opentelemetry/api";
import { provider } from "./provider";


const callService = async (url, spanName) =>
{
  const webTracerWithZone = provider.getTracer("web-app");
  const singleSpan = webTracerWithZone.startSpan(spanName);
  await context.with(trace.setSpan(context.active(), singleSpan), async () => {
    try
    {
      const response = await fetch(url);

      if (!response.ok) {
        throw new Error(`Ошибка HTTP: ${response.status}`);
      }
    }
    catch(error)
    {
      trace.getSpan(context.active()).addEvent("ERROR");

      singleSpan.setStatus({
        code: SpanStatusCode.ERROR,
        message: error.message,
      });

      throw error;
    }
    finally {
      singleSpan.end();
    }
  });
}

const App = () => {
  return (
    <React.Fragment>
      <h1>Hello React</h1>
      <ul>
        <li>
          <button onClick={async () => {
            await callService("/api/servicea/a", "PathA");
          }}>Path A</button>
        </li>
        <li>
          <button onClick={() => {callService("/api/servicea/b", "PathB")}}>Path B</button>
        </li>
      </ul>
    </React.Fragment>
  );
};

export default App;