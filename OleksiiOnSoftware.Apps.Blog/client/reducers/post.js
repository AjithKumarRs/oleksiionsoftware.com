import _ from 'lodash'

const initialState = {
  isInited: false,
  isSidebarOpen: true
}

export default (state = initialState, action) => {
  switch (action.type) {
    case 'POST_INIT_SUCCESS':
      return {
        ...state,
        ...action.payload.data
      }

    default: return state
  }
}
