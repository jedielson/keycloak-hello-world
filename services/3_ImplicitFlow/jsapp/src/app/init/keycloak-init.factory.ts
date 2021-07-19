import { KeycloakService } from "keycloak-angular";

export function initializeKeycloak(
    keycloak: KeycloakService
) {
    return () =>
        keycloak.init({
            config: {
                url: 'http://localhost:8082/auth',
                realm: 'local',
                clientId: 'frontend',
            },
            bearerExcludedUrls: ['/assets'],
        });
}