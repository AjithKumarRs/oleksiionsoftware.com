/* @flow */
/* eslint-disable */

// Domain Model
export type Category = {
  id: string,
  title: string
}

export type Tag = {
  id: string,
  title: string
}

export type Link = {
  id: string,
  title: string
}

export type Post = {
  id: string,
  title: string,
  short: string,
  body: string,
  date: string,
  infobar: boolean,
  category: Category,
  tags: Tag[]
}

export type Filter = {
  +by: string,
  +title ?: string
}

// States
export type HomeState = {
  +isInited: boolean,
  +pageIndex: number,
  +pageSize: number,
  +postsCount: number,
  +pagesCount: number,
  +brand: string,
  +copyright: string,
  +filter: Filter,
  +posts: Post[],
  +links: Link[]
}

export type ConfigState = {
  +hostname: string,
  +isSidebarOpen: boolean,
}

export type CategoryState = {

}

export type RootState = {
  +home: HomeState,
  +category: CategoryState,
  +config: ConfigState
}

// Server responses
export type HomeEndpointServerResponse = {
  id: string,
  brand: string,
  copyright: string,
  filter: Filter,
  links: Link[],
  posts: Post[],
  postsCount: number,
  pageIndex: number,
  pageSize: number,
  pagesCount: number
}

export type PostEndpointServerResponse = {
  id: string,
  brand: string,
  copyright: string,
  links: Link[],
  posts: Post[]
}

export type CategoryEndpointServerResponse = {}

// Actions

// Home Init
export type HomeInitAction = { type: 'HOME_INIT' }
export type HomeInitProgressAction = { type: 'HOME_INIT_PROGRESS' }
export type HomeInitSuccessAction = { type: 'HOME_INIT_SUCCESS', payload: { data: HomeEndpointServerResponse } }
export type HomeInitFailAction = { type: 'HOME_INIT_FAIL' }
export type HomeInitActions = HomeInitAction | HomeInitProgressAction | HomeInitSuccessAction | HomeInitFailAction

// Home Change 
export type HomeChangePageAction = { type: 'HOME_CHANGE_PAGE' }
export type HomeChangePageProgressAction = { type: 'HOME_CHANGE_PAGE_PROGRESS' }
export type HomeChangePageSuccessAction = { type: 'HOME_CHANGE_PAGE_SUCCESS', payload: { data: HomeEndpointServerResponse } }
export type HomeChangePageFailAction = { type: 'HOME_CHANGE_PAGE_FAIL' }
export type HomeChangePageActions = HomeChangePageAction | HomeChangePageProgressAction | HomeChangePageSuccessAction | HomeChangePageFailAction

// Home Filter By Category
export type HomeFilterByCategoryAction = { type: 'HOME_FILTER_BY_CATEGORY' }
export type HomeFilterByCategoryProgressAction = { type: 'HOME_FILTER_BY_CATEGORY_PROGRESS' }
export type HomeFilterByCategorySuccessAction = { type: 'HOME_FILTER_BY_CATEGORY_SUCCESS', payload: { category: string, data: HomeEndpointServerResponse } }
export type HomeFilterByCategoryFailAction = { type: 'HOME_FILTER_BY_CATEGORY_FAIL' }
export type HomeFilterByCategoryActions = HomeFilterByCategoryAction | HomeFilterByCategoryProgressAction | HomeFilterByCategorySuccessAction | HomeFilterByCategoryFailAction

// Home Filter By Date
export type HomeFilterByDateAction = { type: 'HOME_FILTER_BY_DATE' }
export type HomeFilterByDateProgress = { type: 'HOME_FILTER_BY_DATE_PROGRESS' }
export type HomeFilterByDateSuccess = { type: 'HOME_FILTER_BY_DATE_SUCCESS', payload: { date: string, data: HomeEndpointServerResponse } }
export type HomeFilterByDateFail = { type: 'HOME_FILTER_BY_DATE_FAIL' }
export type HomeFilterByDateActions = HomeFilterByDateAction | HomeFilterByDateProgress | HomeFilterByDateSuccess | HomeFilterByDateFail

// Home Filter By Tag
export type HomeFilterByTagAction = { type: 'HOME_FILTER_BY_TAG' }
export type HomeFilterByTagProgressAction = { type: 'HOME_FILTER_BY_TAG_PROGRESS' }
export type HomeFilterByTagSuccessAction = { type: 'HOME_FILTER_BY_TAG_SUCCESS', payload: { tag: string, data: HomeEndpointServerResponse } }
export type HomeFilterByTagFailAction = { type: 'HOME_FILTER_BY_TAG_FAIL' }
export type HomeFilterByTagActions = HomeFilterByTagAction | HomeFilterByTagProgressAction | HomeFilterByTagSuccessAction | HomeFilterByTagFailAction

// Post Init
export type PostInitAction = { type: 'POST_INIT' }
export type PostInitProgressAction = { type: 'POST_INIT_PROGRESS' }
export type PostInitSuccessAction = { type: 'POST_INIT_SUCCESS', payload: { data: PostEndpointServerResponse } }
export type PostInitFailAction = { type: 'POST_INIT_FAIL' }
export type PostInitActions = PostInitAction | PostInitProgressAction | PostInitSuccessAction | PostInitFailAction

// Category Init
export type CategoryInitAction = { type: 'CATEGORY_INIT' }
export type CategoryInitProgressAction = { type: 'CATEGORY_INIT_PROGRESS' }
export type CategoryInitSuccessAction = { type: 'CATEGORY_INIT_SUCCESS', payload: { data: CategoryEndpointServerResponse } }
export type CategoryInitFailAction = { type: 'CATEGORY_INIT_FAIL' }
export type CategoryInitActions = CategoryInitAction | CategoryInitProgressAction | CategoryInitSuccessAction | CategoryInitFailAction

// Misc
export type ToggleSidebarAction = { type: 'TOGGLE_SIDEBAR' }

export type Action =
  HomeInitActions |
  HomeChangePageActions |
  HomeFilterByCategoryActions |
  HomeFilterByDateActions |
  HomeFilterByTagActions |
  CategoryInitActions |
  PostInitActions |
  ToggleSidebarAction

// Redux
export type GetState = () => RootState
export type PromiseAction = Promise<Action>
export type ThunkAction = (dispatch: Dispatch, getState: GetState) => any
export type Dispatch = (action: Action | PromiseAction | ThunkAction | Array<Action>) => any
