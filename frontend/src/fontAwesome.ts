import type { App } from "vue";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";

import { library } from "@fortawesome/fontawesome-svg-core";
import {
  faArrowRightFromBracket,
  faArrowRightToBracket,
  faArrowUpRightFromSquare,
  faCheck,
  faFloppyDisk,
  faKey,
  faPaperPlane,
  faPlus,
  faUser,
} from "@fortawesome/free-solid-svg-icons";

library.add(faArrowRightFromBracket, faArrowRightToBracket, faArrowUpRightFromSquare, faCheck, faFloppyDisk, faKey, faPlus, faPaperPlane, faUser);

export default function (app: App) {
  app.component("font-awesome-icon", FontAwesomeIcon);
}
