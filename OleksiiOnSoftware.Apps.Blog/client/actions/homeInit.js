/* @flow */

// Libs
import { api } from 'utils'

// Types
import type {
  GetState,
  Dispatch,
  ThunkAction,

  HomeInitAction,
  HomeInitActions,
  HomeInitProgressAction,
  HomeInitSuccessAction,
  HomeInitFailAction,

  HomeEndpointServerResponse
} from 'types'

// Actions
export const homeInitAsync = (): ThunkAction => async (dispatch: Dispatch, getState: GetState): Promise<HomeInitActions> => {
  try {
    dispatch(homeInit())

    dispatch(homeInitProgress())

    const state = getState()
    const json = await api()
      .fromRoot()
      .addPath('api/blogs')
      .addPath(state.config.hostname)
      .fetch()

    return dispatch(homeInitSuccess(json))
  } catch (ex) {
    console.error(ex)
    return dispatch(homeInitFail())
  }
}

const homeInit = (): HomeInitAction => ({
  type: 'HOME_INIT'
})

const homeInitProgress = (): HomeInitProgressAction => ({
  type: 'HOME_INIT_PROGRESS'
})

const homeInitSuccess = (data: HomeEndpointServerResponse): HomeInitSuccessAction => ({
  type: 'HOME_INIT_SUCCESS',
  payload: {
    data
  }
})

const homeInitFail = (): HomeInitFailAction => ({
  type: 'HOME_INIT_FAIL'
})
