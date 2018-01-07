/* @flow */

import { combineReducers } from 'redux'
import { routerReducer } from 'react-router-redux'

import home from './home'
import post from './post'
import category from './category'
import config from './config'

const reducer = combineReducers({
  home: home,
  post: post,
  category: category,
  config: config,
  router: routerReducer
})

export default reducer
