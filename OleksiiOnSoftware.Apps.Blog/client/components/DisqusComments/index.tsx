import * as React from "react";

declare var window: any;
declare var DISQUS: any;
declare var document: any;

interface Props {
  id: string;
}

export class DisqusComments extends React.Component<Props> {
  componentDidMount() {
    window.disqus_config = function() {
      this.page.url = `http://oleksiionsoftware.com/post/${this.props.id}`;
      this.page.identifier = this.props.id;
    };

    if (typeof DISQUS !== "undefined") {
      DISQUS.reset({
        reload: true,
        config: function() {
          this.page.url = `http://oleksiionsoftware.com/post/${this.props.id}`;
          this.page.identifier = this.props.id;
        }
      });
    } else {
      const d = document;
      const s = d.createElement("script");
      const timestamp = new Date().getTime().toString();

      s.src = "//oleksiionsoftware.disqus.com/embed.js";
      s.setAttribute("data-timestamp", timestamp);
      (d.head || d.body).appendChild(s);
    }
  }

  render() {
    return <div id="disqus_thread" />;
  }
}
