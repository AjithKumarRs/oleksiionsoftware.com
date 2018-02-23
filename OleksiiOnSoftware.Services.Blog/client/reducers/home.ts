import { HomeState, Action } from "types";

const initialState: HomeState = {
  isInited: false,
  pageIndex: 0,
  pageSize: 10,
  postsCount: 0,
  pagesCount: 0,
  brand: "",
  copyright: "",
  posts: [],
  filter: {
    by: "none"
  },
  links: []
};

export default (state: HomeState = initialState, action: Action): HomeState => {
  switch (action.type) {
    case "HOME_INIT_SUCCESS":
      return {
        ...state,
        filter: {
          by: "none"
        },
        ...action.payload.data
      };

    case "HOME_FILTER_BY_DATE_SUCCESS":
      return {
        ...state,
        filter: {
          by: "date",
          title: action.payload.date.toString()
        },
        ...action.payload.data
      };

    case "HOME_FILTER_BY_CATEGORY_SUCCESS":
      return {
        ...state,
        filter: {
          by: "category",
          title: action.payload.category
        },
        ...action.payload.data
      };

    case "HOME_FILTER_BY_TAG_SUCCESS":
      return {
        ...state,
        filter: {
          by: "tag",
          title: action.payload.tag
        },
        ...action.payload.data
      };

    case "HOME_CHANGE_PAGE_SUCCESS":
      return {
        ...state,
        filter: {
          by: "page",
          title: "page"
        },
        ...action.payload.data
      };

    default:
      return state;
  }
};
