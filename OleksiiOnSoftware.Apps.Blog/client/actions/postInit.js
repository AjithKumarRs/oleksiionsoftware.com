/* @flow */

// Libs
import { api } from 'utils'

// Types
import type {
  GetState,
  Dispatch,
  ThunkAction,

  PostInitAction,
  PostInitActions,
  PostInitProgressAction,
  PostInitSuccessAction,
  PostInitFailAction,

  PostEndpointServerResponse
} from 'types'

// Actions
export const postInitAsync = (postId: string): ThunkAction => async (dispatch: Dispatch, getState: GetState): Promise<PostInitActions> => {
  try {
    dispatch(postInit())

    dispatch(postInitProgress())

    const state = getState()
    const json = await api()
      .fromRoot()
      .addPath('api/blogs')
      .addPath(state.config.hostname)
      .addPath('posts')
      .addPath(postId)
      .fetch()

    return dispatch(postInitSuccess(json))
  } catch (ex) {
    console.error(ex)
    return dispatch(postInitFail())
  }
}

const postInit = (): PostInitAction => ({
  type: 'POST_INIT'
})

const postInitProgress = (): PostInitProgressAction => ({
  type: 'POST_INIT_PROGRESS'
})

const postInitSuccess = (data: PostEndpointServerResponse): PostInitSuccessAction => ({
  type: 'POST_INIT_SUCCESS',
  payload: {
    data
  }
})

const postInitFail = (): PostInitFailAction => ({
  type: 'POST_INIT_FAIL'
})
