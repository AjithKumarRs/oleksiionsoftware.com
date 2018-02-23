// Domain Model
export interface Link {
  id: string;
  title: string;
}

export interface Category {
  id: string;
  title: string;
}

export interface Tag {
  id: string;
  title: string;
}

export interface Filter {
  by: string;
  title?: string;
}

export interface Post {
  id: string;
  title: string;
  short: string;
  body: string;
  infobar: boolean;
  date: string;
  tags: Tag[];
  category: Category;
}

export interface ConfigState {
  readonly hostname: string;
  readonly isSidebarOpen: boolean;
}

// States
export interface HomeState {
  readonly isInited: boolean;
  readonly pageIndex: number;
  readonly pageSize: number;
  readonly postsCount: number;
  readonly pagesCount: number;
  readonly brand: string;
  readonly copyright: string;
  readonly filter: Filter;
  readonly posts: Post[];
  readonly links: Link[];
}

export interface PostState {
  readonly id: string;
  readonly brand: string;
  readonly copyright: string;
  readonly avatar: string;
  readonly github: string;
  readonly linkedin: string;
  readonly twitter: string;
  readonly url: string;
  readonly title: string;
  readonly body: string;
  readonly short: string;
  readonly date: string;
  readonly infobar: boolean;
  readonly comments: boolean;
  readonly category: Category;
  readonly tags: Tag[];
  readonly links: Link[];
}

export interface RootState {
  readonly home: HomeState;
  readonly post: PostState;
  readonly config: ConfigState;
}

// Server responses
export type HomeEndpointServerResponse = {
  brand: string;
  copyright: string;
  filter: Filter;
  links: Link[];
  posts: Post[];
  postsCount: number;
  pageIndex: number;
  pageSize: number;
  pagesCount: number;
};

export type PostEndpointServerResponse = {
  id: string;
  brand: string;
  copyright: string;
  links: Link[];
  posts: Post[];
};

export type CategoryEndpointServerResponse = {};

// Home Init
export type HomeInitAction = { type: "HOME_INIT" };
export type HomeInitProgressAction = { type: "HOME_INIT_PROGRESS" };
export type HomeInitSuccessAction = {
  type: "HOME_INIT_SUCCESS";
  payload: { data: HomeEndpointServerResponse };
};
export type HomeInitFailAction = { type: "HOME_INIT_FAIL" };
export type HomeInitActions =
  | HomeInitAction
  | HomeInitProgressAction
  | HomeInitSuccessAction
  | HomeInitFailAction;

// Home Change
export type HomeChangePageAction = { type: "HOME_CHANGE_PAGE" };
export type HomeChangePageProgressAction = {
  type: "HOME_CHANGE_PAGE_PROGRESS";
};
export type HomeChangePageSuccessAction = {
  type: "HOME_CHANGE_PAGE_SUCCESS";
  payload: { data: HomeEndpointServerResponse };
};
export type HomeChangePageFailAction = { type: "HOME_CHANGE_PAGE_FAIL" };
export type HomeChangePageActions =
  | HomeChangePageAction
  | HomeChangePageProgressAction
  | HomeChangePageSuccessAction
  | HomeChangePageFailAction;

// Home Filter By Category
export type HomeFilterByCategoryAction = { type: "HOME_FILTER_BY_CATEGORY" };
export type HomeFilterByCategoryProgressAction = {
  type: "HOME_FILTER_BY_CATEGORY_PROGRESS";
};
export type HomeFilterByCategorySuccessAction = {
  type: "HOME_FILTER_BY_CATEGORY_SUCCESS";
  payload: { category: string; data: HomeEndpointServerResponse };
};
export type HomeFilterByCategoryFailAction = {
  type: "HOME_FILTER_BY_CATEGORY_FAIL";
};
export type HomeFilterByCategoryActions =
  | HomeFilterByCategoryAction
  | HomeFilterByCategoryProgressAction
  | HomeFilterByCategorySuccessAction
  | HomeFilterByCategoryFailAction;

// Home Filter By Date
export type HomeFilterByDateAction = { type: "HOME_FILTER_BY_DATE" };
export type HomeFilterByDateProgress = { type: "HOME_FILTER_BY_DATE_PROGRESS" };
export type HomeFilterByDateSuccess = {
  type: "HOME_FILTER_BY_DATE_SUCCESS";
  payload: { date: string; data: HomeEndpointServerResponse };
};
export type HomeFilterByDateFail = { type: "HOME_FILTER_BY_DATE_FAIL" };
export type HomeFilterByDateActions =
  | HomeFilterByDateAction
  | HomeFilterByDateProgress
  | HomeFilterByDateSuccess
  | HomeFilterByDateFail;

// Home Filter By Tag
export type HomeFilterByTagAction = { type: "HOME_FILTER_BY_TAG" };
export type HomeFilterByTagProgressAction = {
  type: "HOME_FILTER_BY_TAG_PROGRESS";
};
export type HomeFilterByTagSuccessAction = {
  type: "HOME_FILTER_BY_TAG_SUCCESS";
  payload: { tag: string; data: HomeEndpointServerResponse };
};
export type HomeFilterByTagFailAction = { type: "HOME_FILTER_BY_TAG_FAIL" };
export type HomeFilterByTagActions =
  | HomeFilterByTagAction
  | HomeFilterByTagProgressAction
  | HomeFilterByTagSuccessAction
  | HomeFilterByTagFailAction;

// Post Init
export type PostInitAction = { type: "POST_INIT" };
export type PostInitProgressAction = { type: "POST_INIT_PROGRESS" };
export type PostInitSuccessAction = {
  type: "POST_INIT_SUCCESS";
  payload: { data: PostEndpointServerResponse };
};
export type PostInitFailAction = { type: "POST_INIT_FAIL" };
export type PostInitActions =
  | PostInitAction
  | PostInitProgressAction
  | PostInitSuccessAction
  | PostInitFailAction;

// Category Init
export type CategoryInitAction = { type: "CATEGORY_INIT" };
export type CategoryInitProgressAction = { type: "CATEGORY_INIT_PROGRESS" };
export type CategoryInitSuccessAction = {
  type: "CATEGORY_INIT_SUCCESS";
  payload: { data: CategoryEndpointServerResponse };
};
export type CategoryInitFailAction = { type: "CATEGORY_INIT_FAIL" };
export type CategoryInitActions =
  | CategoryInitAction
  | CategoryInitProgressAction
  | CategoryInitSuccessAction
  | CategoryInitFailAction;

// Misc
export type ToggleSidebarAction = { type: "TOGGLE_SIDEBAR" };

export type Action =
  | HomeInitActions
  | HomeChangePageActions
  | HomeFilterByCategoryActions
  | HomeFilterByDateActions
  | HomeFilterByTagActions
  | CategoryInitActions
  | PostInitActions
  | ToggleSidebarAction;

// Redux
export type GetState = () => RootState;
export type PromiseAction = Promise<Action>;
export type ThunkAction = (dispatch: Dispatch, getState: GetState) => any;
export type Dispatch = (
  action: Action | PromiseAction | ThunkAction | Array<Action>
) => any;
