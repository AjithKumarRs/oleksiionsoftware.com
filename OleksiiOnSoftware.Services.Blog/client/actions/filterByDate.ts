// Libs
import { api } from "utils";

// Types
import {
  Dispatch,
  GetState,
  ThunkAction,
  HomeEndpointServerResponse,
  HomeFilterByDateAction,
  HomeFilterByDateProgress,
  HomeFilterByDateSuccess,
  HomeFilterByDateFail
} from "types";

// Actions
export const homeFilterByDateAsync = (date: string): ThunkAction => async (
  dispatch: Dispatch,
  getState: GetState
) => {
  try {
    dispatch(homeFilterByDate());

    dispatch(homeFilterByDateProgress());

    const params = {
      filterByDate: date
    };

    const state = getState();
    const json = await api()
      .fromRoot()
      .addPath("api/blogs")
      .addPath(state.config.hostname)
      .setParams(params)
      .fetch();

    return dispatch(homeFilterByDateSuccess(date, json));
  } catch (ex) {
    console.error(ex);
    return dispatch(homeFilterByDateFail());
  }
};

const homeFilterByDate = (): HomeFilterByDateAction => ({
  type: "HOME_FILTER_BY_DATE"
});

const homeFilterByDateProgress = (): HomeFilterByDateProgress => ({
  type: "HOME_FILTER_BY_DATE_PROGRESS"
});

const homeFilterByDateSuccess = (
  date: string,
  data: HomeEndpointServerResponse
): HomeFilterByDateSuccess => ({
  type: "HOME_FILTER_BY_DATE_SUCCESS",
  payload: {
    date,
    data
  }
});

const homeFilterByDateFail = (): HomeFilterByDateFail => ({
  type: "HOME_FILTER_BY_DATE_FAIL"
});
