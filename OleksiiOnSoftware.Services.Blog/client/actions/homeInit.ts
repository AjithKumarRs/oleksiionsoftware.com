// Libs
import { api } from "utils";

// Actions
import { menuInitSuccess } from "./menu";

// Types
import {
  Dispatch,
  GetState,
  ThunkAction,
  HomeInitAction,
  HomeInitProgressAction,
  HomeInitSuccessAction,
  HomeEndpointServerResponse,
  HomeInitFailAction
} from "types";

// Actions
export const homeInitAsync = (): ThunkAction => async (dispatch: Dispatch, getState: GetState) => {
  try {
    dispatch(homeInit());

    dispatch(homeInitProgress());

    const state = getState();
    const json = await api()
      .fromRoot()
      .addPath("api/blogs")
      .addPath(state.config.hostname)
      .fetch();

    dispatch(menuInitSuccess(json));
    return dispatch(homeInitSuccess(json));
  } catch (ex) {
    console.error(ex);
    return dispatch(homeInitFail());
  }
};

const homeInit = (): HomeInitAction => ({
  type: "HOME_INIT"
});

const homeInitProgress = (): HomeInitProgressAction => ({
  type: "HOME_INIT_PROGRESS"
});

const homeInitSuccess = (data: HomeEndpointServerResponse): HomeInitSuccessAction => ({
  type: "HOME_INIT_SUCCESS",
  payload: {
    data
  }
});

const homeInitFail = (): HomeInitFailAction => ({
  type: "HOME_INIT_FAIL"
});
