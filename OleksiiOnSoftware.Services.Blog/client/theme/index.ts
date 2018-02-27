import * as styledComponents from "styled-components";

export interface IThemeInterface {
  primaryColor: string;
}

const {
  default: styled,
  css,
  injectGlobal,
  keyframes,
  ThemeProvider
} = styledComponents as styledComponents.ThemedStyledComponentsModule<any> as styledComponents.ThemedStyledComponentsModule<IThemeInterface>;

export const theme = {
  primaryColor: "#e9e9eb"
};

export default styled;

export { css, injectGlobal, keyframes, ThemeProvider };