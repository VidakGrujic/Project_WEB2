import React from "react";

export default function UploadImage({ slika, setSlika }) {
  const handleImageLoad = async (e) => {
    const base64 = await convertBase64(e.target.files[0]);
    setSlika(base64);
  };

  const convertBase64 = (file) => {
    return new Promise((resolve, reject) => {
      const fileReader = new FileReader();
      fileReader.readAsDataURL(file);

      fileReader.onload = () => {
        resolve(fileReader.result);
      };

      fileReader.onerror = (error) => {
        reject(error);
      };
    });
  };

  return (
    <>
      <div className="field">
        <img
          className="ui centered medium image"
          src={slika}
          alt="file preview"
        ></img>
        {/*error && fotografija ?  <div className="ui pointing red basic label">Morate odabrati fotografiju</div> : null*/}
      </div>
      <div>
        <input
          type="file"
          className="custom-file-input"
          onChange={handleImageLoad}
        ></input>
      </div>
    </>
  );
}
