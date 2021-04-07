import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router, NavigationEnd } from '@angular/router';
import { IBreadCrumb } from './breadcrumb.interface';
import { filter, distinctUntilChanged } from 'rxjs/operators';

@Component({
  selector: 'app-breadcrumb',
  templateUrl: './breadcrumb.component.html',
  styleUrls: ['./breadcrumb.component.scss']
})
export class BreadcrumbComponent implements OnInit {
  public breadcrumbs: IBreadCrumb[];

  constructor(
    private router: Router,
    private activatedRoute: ActivatedRoute,
  ) {
    this.breadcrumbs = this.buildBreadCrumb(this.activatedRoute.root);
  }

  ngOnInit() {
    this.router.events.pipe(
      filter((event) => event instanceof NavigationEnd),
      distinctUntilChanged(),
    ).subscribe(() => {
      this.breadcrumbs = this.buildBreadCrumb(this.activatedRoute.root);
    })
  }

  buildBreadCrumb(route: ActivatedRoute, url: string = '', breadcrumbs: IBreadCrumb[] = []): IBreadCrumb[] {
    //If no routeConfig is avalailable we are on the root path
    let label = route.routeConfig && route.routeConfig.data ? route.routeConfig.data.breadcrumb : '';
    let path = route.routeConfig && route.routeConfig.data ? route.routeConfig.path : '';

    const lastRoutePart = path.split('/').pop();
    const isCategory = lastRoutePart.includes(':idLanguageToLearned');
    const isTest = lastRoutePart.includes(':subCategoryNameTest');
    const isDynamicRoute = lastRoutePart.startsWith(':');
    if(isDynamicRoute && !!route.snapshot && (!isCategory && !isTest)) {
      const paramName = lastRoutePart.split(':')[1];
      label = route.snapshot.params[paramName];
    }


    const routes = path.split('/');
    const lastRoute = routes.pop();
    const firstRoute = routes.pop();
    if(isDynamicRoute && !!route.snapshot) {
      const firstParamName = firstRoute.split(':')[1];
      const lastParamName = lastRoute.split(':')[1];
      path = firstParamName == undefined ? path.replace(lastRoute, route.snapshot.params[lastParamName])
        : path.replace(lastRoute, route.snapshot.params[lastParamName]).replace(firstRoute, route.snapshot.params[firstParamName]);
      }

    //In the routeConfig the complete path is not available,
    //so we rebuild it each time
    const nextUrl = path ? `${url}/${path}` : url;

    const breadcrumb: IBreadCrumb = {
      label: label,
      url: nextUrl,
    };
    // Only adding route with non-empty label
    const newBreadcrumbs = breadcrumb.label ? [ ...breadcrumbs, breadcrumb ] : [ ...breadcrumbs];
    if (route.firstChild) {
      //If we are not on our current path yet,
      //there will be more children to look after, to build our breadcumb
      return this.buildBreadCrumb(route.firstChild, nextUrl, newBreadcrumbs);
    }
    return newBreadcrumbs;
  }

  backButtonHandle(): void {
    if (this.breadcrumbs.length > 1) {
      this.breadcrumbs.pop();
      this.router.navigate([this.breadcrumbs[this.breadcrumbs.length-1].url])
    }
  }
}
