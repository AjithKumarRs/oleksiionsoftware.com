// Framework
import * as React from "react";

// UI
import { Menu, Icon } from "semantic-ui-react";

// Styles
const locals = require("./styles.css");

// Types
import { Link } from "types";

interface Props {
  brand: string;
  links: Link[];
  onLinkClick: (link: Link) => void;
}

// Components
export class LeftNavigation extends React.Component<Props> {
  handleLinkClick = (lnk: Link) => {
    this.props.onLinkClick(lnk);
  };

  render() {
    return (
      <div className={locals.navigation}>
        <div className={locals.brand}>
          <Menu.Item className={locals.text}>{this.props.brand}</Menu.Item>
        </div>

        <div className={locals.links}>
          {this.props.links &&
            this.props.links.map(lnk => (
              <Menu.Item
                key={lnk.id}
                name="cubes"
                className={locals.link}
                onClick={() => this.handleLinkClick(lnk)}
                link
              >
                {lnk.title}
              </Menu.Item>
            ))}
        </div>

        <div className={locals.icons}>
          <a href="https://github.com/oleksii-udovychenko">
            <Icon name="github" size="large" inverted />
          </a>
          <a href="https://www.linkedin.com/in/oleksii-udovychenko">
            <Icon name="linkedin" size="large" inverted />
          </a>
          <a href="https://twitter.com/boades_net">
            <Icon name="twitter" size="large" inverted />
          </a>
        </div>

        <div className={locals.copyright}>
          <div className={locals["copyright-text"]}>
            <div>
              Copyright{" "}
              <Icon
                name="copyright"
                className={locals["copyright-icon"]}
                size="small"
                inverted
              />
            </div>
            <div>Oleksii Udovychenko</div>
            <div>2010 - 2017</div>
          </div>
        </div>
      </div>
    );
  }
}
