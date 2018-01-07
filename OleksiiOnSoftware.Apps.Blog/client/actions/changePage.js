/* @flow */

// Libs
import { api } from 'utils'

// Types
import type {
  GetState,
  Dispatch,
  ThunkAction,

  HomeChangePageAction,
  HomeChangePageActions,
  HomeChangePageProgressAction,
  HomeChangePageSuccessAction,
  HomeChangePageFailAction,

  HomeEndpointServerResponse
} from 'types'

// Actions
export const homeChangePageAsync = (pageIndex: number, pageSize: number): ThunkAction => async (dispatch: Dispatch, getState: GetState): Promise<HomeChangePageActions> => {
  try {
    dispatch(homeChangePage())

    dispatch(homeChangePageProgress())

    const params = {
      pageIndex: pageIndex,
      pageSize: pageSize
    }

    const state = getState()
    const json = await api()
      .fromRoot()
      .addPath('api/blogs')
      .addPath(state.config.hostname)
      .setParams(params)
      .fetch()

    return dispatch(homeChangePageSuccess(json))
  } catch (ex) {
    console.error(ex)
    return dispatch(homeChangePageFail())
  }
}

const homeChangePage = () : HomeChangePageAction => ({
  type: 'HOME_CHANGE_PAGE'
})

const homeChangePageProgress = (): HomeChangePageProgressAction => ({
  type: 'HOME_CHANGE_PAGE_PROGRESS'
})

const homeChangePageSuccess = (data: HomeEndpointServerResponse): HomeChangePageSuccessAction => ({
  type: 'HOME_CHANGE_PAGE_SUCCESS',
  payload: {
    data
  }
})

const homeChangePageFail = (): HomeChangePageFailAction => ({
  type: 'HOME_CHANGE_PAGE_FAIL'
})
