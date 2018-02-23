import { ConfigState, Action } from "types";

const initialState: ConfigState = {
  hostname: "",
  isSidebarOpen: true
};

export default (
  state: ConfigState = initialState,
  action: Action
): ConfigState => {
  switch (action.type) {
    case "TOGGLE_SIDEBAR":
      return {
        ...state,
        isSidebarOpen: !state.isSidebarOpen
      };

    default:
      return state;
  }
};
