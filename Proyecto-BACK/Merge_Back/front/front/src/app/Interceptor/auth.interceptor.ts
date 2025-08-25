import { HttpInterceptorFn } from '@angular/common/http';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  if (req.url.includes("Login")) return next(req);

  const token = localStorage.getItem('token');
  const dbProvider = localStorage.getItem('dbProvider') || 'SqlServer';

  const cloneRequest = req.clone({
    setHeaders: {
      Authorization: `Bearer ${token}`,
      'X-Db-Provider': dbProvider
    }
  });

  return next(cloneRequest);
};
