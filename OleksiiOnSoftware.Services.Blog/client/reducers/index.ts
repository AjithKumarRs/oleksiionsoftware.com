import { combineReducers } from "redux";
import { routerReducer } from "react-router-redux";

import home from "./home";
import post from "./post";
import config from "./config";

const reducer = combineReducers({
  home: home,
  post: post,
  config: config,
  router: routerReducer
});

export default reducer;
