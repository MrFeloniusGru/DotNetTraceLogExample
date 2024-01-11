import React from "react";
import ReactDOM from "react-dom";
import App from "./App";
import "./App.scss";

import { Resource } from "@opentelemetry/resources";
import { WebTracerProvider } from "@opentelemetry/sdk-trace-web";
import { ConsoleSpanExporter, SimpleSpanProcessor } from "@opentelemetry/sdk-trace-base";
import { SemanticResourceAttributes } from "@opentelemetry/semantic-conventions";
import { CollectorTraceExporter } from "@opentelemetry/exporter-collector";
import { ZoneContextManager } from "@opentelemetry/context-zone";
import { B3Propagator, B3InjectEncoding } from "@opentelemetry/propagator-b3";
import { registerInstrumentations } from "@opentelemetry/instrumentation";
import { FetchInstrumentation } from "@opentelemetry/instrumentation-fetch";


export const provider = new WebTracerProvider({
    resource: new Resource({
      [SemanticResourceAttributes.SERVICE_NAME]: "web-app",
    }),
  });
  
const exporter = new CollectorTraceExporter({
    url: "/v1/traces",
  });
  
provider.addSpanProcessor(new SimpleSpanProcessor(new ConsoleSpanExporter()));
provider.addSpanProcessor(new SimpleSpanProcessor(exporter));
  
provider.register({
  contextManager: new ZoneContextManager(),
  propagator: new B3Propagator({
    injectEncoding: B3InjectEncoding.MULTI_HEADER,
  }),
});
  
registerInstrumentations({
  instrumentations: [
    new FetchInstrumentation({
      ignoreUrls: [/localhost:\d{1,5}\/sockjs-node/],
      clearTimingResources: true,
    }),
  ],
  tracerProvider: provider,
});

const el = document.getElementById("app");

ReactDOM.render(<App />, el);