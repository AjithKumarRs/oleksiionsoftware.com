import {
  CATEGORY_INIT,
  CATEGORY_INIT_SUCCESS,
  CATEGORY_INIT_FAIL
} from 'actions'

export default function category (state = {}, action) {
  switch (action.type) {
    case CATEGORY_INIT:
      return state

    case CATEGORY_INIT_SUCCESS:
      return state.merge({
        isInited: true,
        ...action.payload
      })

    case CATEGORY_INIT_FAIL:
      return state.set('title', 'ERROR')

    default: return state
  }
};
