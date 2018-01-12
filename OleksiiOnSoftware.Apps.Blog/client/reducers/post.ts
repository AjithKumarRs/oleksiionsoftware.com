import { PostState, Action } from "types";

const initialState: PostState = {
  brand: "",
  avatar: "",
  comments: true,
  copyright: "",
  github: "",
  linkedin: "",
  twitter: "",
  url: "",
  links: [],
  body: "",
  short: "",
  category: {
    id: "",
    title: ""
  },
  date: "",
  id: "",
  infobar: false,
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
