// Libs
import { api } from "utils";

// Actions
import { menuInitSuccess } from "./menu";

// Types
import {
  Dispatch,
  GetState,
  ThunkAction,
  HomeEndpointServerResponse,
  HomeFilterByCategoryAction,
  HomeFilterByCategoryProgressAction,
  HomeFilterByCategorySuccessAction,
  HomeFilterByCategoryFailAction
} from "types";

// Actions
export const homeFilterByCategoryAsync = (categoryId: string): ThunkAction => async (dispatch: Dispatch, getState: GetState) => {
  try {
    dispatch(homeFilterByCategory());

    dispatch(homeFilterByCategoryProgress());

    const params = {
      filterByCategory: categoryId
    };

    const state = getState();
    const json = await api()
      .fromRoot()
      .addPath("api/blogs")
      .addPath(state.config.hostname)
      .setParams(params)
      .fetch();

    dispatch(menuInitSuccess(json));
    return dispatch(homeFilterByCategorySuccess(categoryId, json));
  } catch (ex) {
    console.error(ex);
    return dispatch(homeFilterByCategoryFail());
  }
};

const homeFilterByCategory = (): HomeFilterByCategoryAction => ({
  type: "HOME_FILTER_BY_CATEGORY"
});

const homeFilterByCategoryProgress = (): HomeFilterByCategoryProgressAction => ({
  type: "HOME_FILTER_BY_CATEGORY_PROGRESS"
});

const homeFilterByCategorySuccess = (category: string, data: HomeEndpointServerResponse): HomeFilterByCategorySuccessAction => ({
  type: "HOME_FILTER_BY_CATEGORY_SUCCESS",
  payload: {
    category,
    data
  }
});

const homeFilterByCategoryFail = (): HomeFilterByCategoryFailAction => ({
  type: "HOME_FILTER_BY_CATEGORY_FAIL"
});
