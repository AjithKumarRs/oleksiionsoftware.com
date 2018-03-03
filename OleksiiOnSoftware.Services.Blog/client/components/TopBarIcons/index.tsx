// Framework
import * as React from "react";
import styled from "theme";

// UI
import { Icon } from "semantic-ui-react";

// Components
interface IconsProps {
  className?: string;
}

const TopBarIcons = ({ className }: IconsProps) => (
  <div className={className}>
    <a href="https://github.com/oleksii-udovychenko">
      <Icon name="github" size="large" />
    </a>
    <a href="https://www.linkedin.com/in/oleksii-udovychenko">
      <Icon name="linkedin" size="large" />
    </a>
    <a href="https://twitter.com/boades_net">
      <Icon name="twitter" size="large" />
    </a>
  </div>
);

const StyledTopBarIcons = styled(TopBarIcons)`
  display: none;
  font-size: 1.2rem;

  @media (min-width: 768px) {
    display: block;
  }

  a {
    margin-left: 5px;
    margin-right: 5px;
  }

  i {
    color: #e8e8e8 !important;
  }

  i:hover {
    color: rgba(0, 0, 0, 0.84) !important;
    transform: scale(1.3);
    cursor: pointer;
  }
`;

export { StyledTopBarIcons as TopBarIcons };
