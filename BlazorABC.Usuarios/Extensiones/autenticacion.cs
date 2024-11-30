using CRUD.Compartir;
using Blazored.SessionStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace BlazorABC.Client.Extensiones
{
    public class autenticacion : AuthenticationStateProvider
    {
        private readonly ISessionStorageService _sessionStorage;

        private ClaimsPrincipal _sininformacion = new ClaimsPrincipal(new ClaimsIdentity());

        public autenticacion( ISessionStorageService sessionStorage )
        {
            _sessionStorage = sessionStorage;

        }

        public async Task ActualizarEstadoAutenticacion(Ssesion? sesionUsuario)
        {
            ClaimsPrincipal claimsPrincipal;

            if(sesionUsuario != null ) {
                claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim("usname",sesionUsuario.usuario),
                    new Claim("usid",sesionUsuario.id.ToString())
                },"JwtAuth"));

                await _sessionStorage.GuardarStorage("sesionUsuario", sesionUsuario);
            }
            else
            {
                claimsPrincipal = _sininformacion;
                await _sessionStorage.RemoveItemAsync("sesionUsuario");
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }


        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var sesionUsuario = await _sessionStorage.ObtenerStorage<Ssesion>("sesionUsuario");

            if(sesionUsuario == null )
            {
                return await Task.FromResult(new AuthenticationState(_sininformacion));
                
            }
            else
            {
                var claimPrincipal= new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                {
                    new Claim("usname",sesionUsuario.usuario),
                    new Claim("usid",sesionUsuario.id.ToString())
                }, "JwtAuth"));

                return await Task.FromResult(new AuthenticationState(claimPrincipal));
            }
        }
    }
}
