import { createStore, applyMiddleware, compose } from 'redux'
import { createLogger } from 'redux-logger'
import thunkMiddleware from 'redux-thunk'

import reducer from 'reducers'
import { DevTools } from 'containers'

import { routerMiddleware } from 'react-router-redux'

const configureStore = (history, initialState) => {
  const logger = createLogger({
    duration: true,
    collapsed: true
  })

  const enhancer = compose(
    applyMiddleware(routerMiddleware(history), logger, thunkMiddleware),
    DevTools.instrument()
  )

  return createStore(reducer, initialState, enhancer)
}

export default configureStore
