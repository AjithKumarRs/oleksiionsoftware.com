import { ConfigState, Action } from "types";

const initialState: ConfigState = {
  hostname: "",
  isMenuExpanded: false,
  avatar: "",
  brand: "",
  copyright: "",
  github: "",
  linkedin: "",
  links: [],
  twitter: ""
};

export default (state: ConfigState = initialState, action: Action): ConfigState => {
  switch (action.type) {
    case "TOGGLE_MENU":
      return {
        ...state,
        isMenuExpanded: !state.isMenuExpanded
      };
    case "MENU_INIT_SUCCESS":
      return {
        ...state,
        ...action.payload.data
      };
    default:
      return state;
  }
};
