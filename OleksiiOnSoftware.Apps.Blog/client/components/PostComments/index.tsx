// Framework
import * as React from "react";

// UI
import { Icon, Label } from "semantic-ui-react";

// Types
interface Props {
  url: string;
}

// TODO: Refactor URL
export class PostComments extends React.Component<Props> {
  render() {
    return (
      <Label>
        <Icon name="comments" />{" "}
        <a
          href={`http://oleksiionsoftware.com/post/${
            this.props.url
          }#disqus_thread`}
        />
      </Label>
    );
  }
}
