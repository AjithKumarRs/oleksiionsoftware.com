import { PostState, Action } from "types";

const initialState: PostState = {
  id: "",
  url: "",
  body: "",
  short: "",
  category: {
    id: "",
    title: ""
  },
  date: "",
  infobar: false,
  comments: true,
  tags: [],
  title: ""
};

export default (state: PostState = initialState, action: Action) => {
  switch (action.type) {
    case "POST_INIT_SUCCESS":
      return {
        ...state,
        ...action.payload.data
      };

    default:
      return state;
  }
};
