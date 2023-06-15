import type { App } from "vue";
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";

import { library } from "@fortawesome/fontawesome-svg-core";
import {
  faArrowRightFromBracket,
  faArrowRightToBracket,
  faArrowUpRightFromSquare,
  faCheck,
  faChessRook,
  faChevronLeft,
  faCircleInfo,
  faEye,
  faEyeSlash,
  faFloppyDisk,
  faHistory,
  faKey,
  faPaperPlane,
  faPlus,
  faRotate,
  faSearch,
  faTimes,
  faTrash,
  faUser,
  faVial,
} from "@fortawesome/free-solid-svg-icons";

library.add(
  faArrowRightFromBracket,
  faArrowRightToBracket,
  faArrowUpRightFromSquare,
  faCheck,
  faChessRook,
  faChevronLeft,
  faCircleInfo,
  faEye,
  faEyeSlash,
  faFloppyDisk,
  faHistory,
  faKey,
  faPaperPlane,
  faPlus,
  faRotate,
  faSearch,
  faTimes,
  faTrash,
  faUser,
  faVial
);

export default function (app: App) {
  app.component("font-awesome-icon", FontAwesomeIcon);
}
