import { JwtStorageService } from './core/services/jwt-storage.service';
import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private jwtStorageService:JwtStorageService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const token=this.jwtStorageService.getToken();
    if (token){
      request=request.clone({
        setHeaders:{Authorization:`Bearer ${token}`}
      })
    }
    return next.handle(request);
  }
}
