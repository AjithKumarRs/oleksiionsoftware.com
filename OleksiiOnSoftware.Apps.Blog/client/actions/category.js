/* @flow */

// Libs
import { api } from 'utils'

// Types
import type {
  GetState,
  Dispatch,
  ThunkAction,

  CategoryInitAction,
  CategoryInitActions,
  CategoryInitProgressAction,
  CategoryInitSuccessAction,
  CategoryInitFailAction,

  CategoryEndpointServerResponse
} from 'types'

// Actions
export const categoryInitAsync = (blogId: string, categoryId: string): ThunkAction => async (dispatch: Dispatch, getState: GetState): Promise<CategoryInitActions> => {
  try {
    dispatch(categoryInit())

    dispatch(categoryInitProgress())

    const params = {
      filterByCategory: categoryId
    }

    const json = await api()
      .fromRoot()
      .addPath('api/blogs')
      .addPath(blogId)
      .setParams(params)
      .toString()

    return dispatch(categoryInitSuccess(json))
  } catch (err) {
    console.error(err)
    return dispatch(categoryInitFail())
  }
}

const categoryInit = (): CategoryInitAction => ({
  type: 'CATEGORY_INIT'
})

const categoryInitProgress = (): CategoryInitProgressAction => ({
  type: 'CATEGORY_INIT_PROGRESS'
})

const categoryInitSuccess = (data: CategoryEndpointServerResponse): CategoryInitSuccessAction => ({
  type: 'CATEGORY_INIT_SUCCESS',
  payload: {
    data
  }
})

const categoryInitFail = (): CategoryInitFailAction => ({
  type: 'CATEGORY_INIT_FAIL'
})
