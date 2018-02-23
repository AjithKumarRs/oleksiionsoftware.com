import * as fetch from "isomorphic-fetch";

declare var window: any;
declare var URLSearchParams: any;

interface UrlBuilderParams {
  baseUrl: string;
  path: string[];
  query: any;
}

class UrlBuilder {
  private params: UrlBuilderParams = {
    baseUrl: "",
    path: [],
    query: {}
  };

  static fromUrl(baseUrl: string) {
    return new UrlBuilder(baseUrl);
  }

  static fromRoot() {
    if (typeof window === 'undefined') {
      return new UrlBuilder(process.env.API_INTERNAL_HOSTNAME)
    }

    return new UrlBuilder(process.env.API_PUBLIC_HOSTNAME);
  }

  constructor(baseUrl: string) {
    this.params.baseUrl = baseUrl;
  }

  addPath(segment: string) {
    this.params.path.push(segment);
    return this;
  }

  setParam(name: string, value: string) {
    this.params.query[name] = value;
    return this;
  }

  setParams(obj: any) {
    for (const key of Object.keys(obj)) {
      this.setParam(key, obj[key]);
    }

    return this;
  }

  async fetch(): Promise<any> {
    const req = await fetch(this.toString());
    if (req.status >= 400) {
      throw Error("Request failed");
    }

    const json = await req.json();
    return json;
  }

  toString() {
    // Start with the base url
    let url = this.params.baseUrl;

    // Add path
    let path = "/";
    if (this.params.path.length > 0) {
      path = "/" + this.params.path.join("/") + "/";
    }

    url += path;
    url = url.trim();

    // Add query string part, if any
    if (Object.keys(this.params.query).length > 0) {
      let searchParams = new URLSearchParams();
      for (let param in this.params.query) {
        searchParams.append(param, this.params.query[param]);
      }

      url += "?" + searchParams.toString();
    }

    return url;
  }
}

const api = (): any => {
  return UrlBuilder;
};

export { api };
