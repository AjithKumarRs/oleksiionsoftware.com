import { createStore, applyMiddleware, compose } from "redux";
import { createLogger } from "redux-logger";
import thunkMiddleware from "redux-thunk";

import reducer from "reducers";
import { DevTools } from "components";

import { routerMiddleware } from "react-router-redux";
import { RootState } from "client/types";

const configureStore = (history: History, initialState: RootState) => {
  const logger = createLogger({
    duration: true,
    collapsed: true
  });

  const enhancer = compose(
    applyMiddleware(routerMiddleware(history as any), logger, thunkMiddleware),
    DevTools.instrument()
  );

  return createStore(reducer, initialState, enhancer);
};

export default configureStore;
