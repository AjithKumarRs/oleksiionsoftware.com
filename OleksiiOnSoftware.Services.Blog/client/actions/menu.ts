import { ToggleMenuAction, MenuInitSuccessAction, ConfigEndpointServerResponse } from "types";

export const toggleMenu = (): ToggleMenuAction => ({
  type: "TOGGLE_MENU"
});

export const menuInitSuccess = (data: ConfigEndpointServerResponse): MenuInitSuccessAction => ({
  type: "MENU_INIT_SUCCESS",
  payload: {
    data
  }
});
