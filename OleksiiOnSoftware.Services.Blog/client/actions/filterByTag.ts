// Libs
import { api } from "utils";

// Actions
import { menuInitSuccess } from "./menu";

// Types
import {
  Dispatch,
  GetState,
  ThunkAction,
  HomeFilterByTagAction,
  HomeFilterByTagProgressAction,
  HomeFilterByTagFailAction,
  HomeFilterByTagSuccessAction,
  HomeEndpointServerResponse
} from "types";

// Actions
export const homeFilterByTagAsync = (tagId: string): ThunkAction => async (dispatch: Dispatch, getState: GetState) => {
  try {
    dispatch(homeFilterByTag());

    dispatch(homeFilterByTagProgress());

    const params = {
      filterByTag: tagId
    };

    const state = getState();
    const json = await api()
      .fromRoot()
      .addPath("api/blogs")
      .addPath(state.config.hostname)
      .setParams(params)
      .fetch();

    dispatch(menuInitSuccess(json));
    return dispatch(homeFilterByTagSuccess(tagId, json));
  } catch (ex) {
    console.error(ex);
    return dispatch(homeFilterByTagFail());
  }
};

const homeFilterByTag = (): HomeFilterByTagAction => ({
  type: "HOME_FILTER_BY_TAG"
});

const homeFilterByTagProgress = (): HomeFilterByTagProgressAction => ({
  type: "HOME_FILTER_BY_TAG_PROGRESS"
});

const homeFilterByTagSuccess = (tag: string, data: HomeEndpointServerResponse): HomeFilterByTagSuccessAction => ({
  type: "HOME_FILTER_BY_TAG_SUCCESS",
  payload: {
    tag,
    data
  }
});

const homeFilterByTagFail = (): HomeFilterByTagFailAction => ({
  type: "HOME_FILTER_BY_TAG_FAIL"
});
