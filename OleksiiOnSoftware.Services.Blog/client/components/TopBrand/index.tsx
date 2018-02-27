// React
import * as React from "react";
import styled from "theme";

// UI
import { Header, Icon } from "semantic-ui-react";

// Types
interface Props {
  className?: string;
  brand: string;
}

// Components
const TopBrand = ({ className, brand }: Props) => (
  <Header className={className} as="h2" textAlign="center">
    <Header.Content>
      <h2>{brand}</h2>
      <Header.Subheader className={"icons"}>
        <a href="https://github.com/oleksii-udovychenko">
          <Icon name="github" size="large" />
        </a>
        <a href="https://www.linkedin.com/in/oleksii-udovychenko">
          <Icon name="linkedin" size="large" />
        </a>
        <a href="https://twitter.com/boades_net">
          <Icon name="twitter" size="large" />
        </a>
      </Header.Subheader>
    </Header.Content>
  </Header>
);

// Styled Components
const StyledTopBrand = styled(TopBrand)`
  height: 75px;
  margin-bottom: 5px !important;

  h2 {
    margin: 0;
    padding: 0;
    font-family: Lato, "Helvetica Neue", Arial, Helvetica, sans-serif;
    font-weight: 600;
    font-style: normal;
    font-size: 30px;
    color: rgba(0, 0, 0, 0.84);
    display: block;
    text-align: justify;
    line-height: 1.5;
    letter-spacing: 0.5;
    word-spacing: 5;
  }

  .icons i {
    color: #e8e8e8 !important;
    margin-left: 5px;
    margin-right: 5px;
  }

  .icons i:hover {
    color: rgba(0, 0, 0, 0.84) !important;
    transform: scale(1.3);
    cursor: pointer;
  }
`;

export { StyledTopBrand as TopBrand };
