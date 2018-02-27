// Framework
import * as React from "react";

// UI
import { Icon, Label } from "semantic-ui-react";

// Types
interface Props {
  url: string;
}

// Components
export class PostComments extends React.Component<Props> {
  render() {
    const url =
      "http://oleksiionsoftware.com/post/" + this.props.url + "#disqus_thread";

    return (
      <Label>
        <Icon name="comments" /> <a href={url}>0 Comments</a>
      </Label>
    );
  }
}
