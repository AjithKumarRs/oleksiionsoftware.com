import React from 'react'

export class DisqusComments extends React.Component {
  componentDidMount () {
    window.disqus_config = function () {
      this.page.url = `http://oleksiionsoftware.com/post/${this.props.id}`
      this.page.identifier = this.props.id
    };

    if(window.DISQUS) {
      DISQUS.reset({
        reload: true,
        config: function () {  
          this.page.url = `http://oleksiionsoftware.com/post/${this.props.id}`
          this.page.identifier = this.props.id
        }
      });
    } else {      
      var d = document, s = d.createElement('script');
      s.src = '//oleksiionsoftware.disqus.com/embed.js';
      s.setAttribute('data-timestamp', +new Date());
      (d.head || d.body).appendChild(s);
    }
  }

  render () {
    return (<div id="disqus_thread"></div>)
  }
}
