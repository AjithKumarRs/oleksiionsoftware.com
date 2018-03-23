import { createStore, applyMiddleware, compose, Middleware } from "redux";
import { createLogger } from "redux-logger";
import thunkMiddleware from "redux-thunk";

import reducer from "reducers";
import { DevTools } from "components";

import { routerMiddleware } from "react-router-redux";
import { RootState } from "client/types";

const configureStore = (history: History, initialState: RootState) => {
  const middlewares : Middleware[] = [];
  middlewares.push(routerMiddleware(history as any));

  if(process.env.NODE_ENV === "development") {
    const logger = createLogger({
      duration: true,
      collapsed: true
    });

    middlewares.push(logger);
  }
 
  middlewares.push(thunkMiddleware);

  const enhancer = compose(
    applyMiddleware(...middlewares),
    DevTools.instrument()
  );

  return createStore(reducer, initialState, enhancer);
};

export default configureStore;
