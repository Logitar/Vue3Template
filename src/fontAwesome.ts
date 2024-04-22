import type { App } from "vue";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";

import { library } from "@fortawesome/fontawesome-svg-core";
import {
  faArrowRightFromBracket,
  faArrowRightToBracket,
  faArrowUpRightFromSquare,
  faHome,
  faKey,
  faSave,
  faTimes,
  faUser,
  faVial,
} from "@fortawesome/free-solid-svg-icons";

library.add(faArrowRightFromBracket, faArrowRightToBracket, faArrowUpRightFromSquare, faHome, faKey, faSave, faTimes, faUser, faVial);

export default function (app: App) {
  app.component("font-awesome-icon", FontAwesomeIcon);
}
