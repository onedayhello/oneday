"use client";

import Link from "next/link";
import { Formik, Form, Field, FormikHelpers } from "formik";
import Auth from "@/util/Auth";
import FormField from "./FormField";
import { useRouter } from "next/navigation";
import { AppRouterInstance } from "next/dist/shared/lib/app-router-context";

interface Values {
  username: string;
  password: string;
  passwordConfirm: string;
  accept: boolean;
}

interface Errors {
  username?: string;
  password?: string;
  passwordConfirm?: string;
  accept?: string;
}

const handleSubmit = async (
  values: Values,
  { setSubmitting }: FormikHelpers<Values>,
  router: AppRouterInstance
) => {
  const body = { username: values.username, password: values.password };

  const signupRes = await Auth.signup(body);
  setSubmitting(false);

  if (!signupRes) {
    // TODO: signup failure handling
    return;
  }

  router.push("/auth/login");
};

const validate = (values: Values) => {
  const errors: Errors = {};

  const required = "Required";

  if (!values.username) {
    errors.username = required;
  }

  if (!values.password) {
    errors.password = required;
  }

  if (!values.passwordConfirm) {
    errors.passwordConfirm = required;
  } else if (values.password != values.passwordConfirm) {
    errors.passwordConfirm = "Password does not match";
  }

  if (!values.accept) {
    errors.accept = required;
  }

  return errors;
};

const SignupForm = () => {
  const router = useRouter();

  return (
    <div className="flex flex-col justify-center">
      <h1 className="text-center text-gray-800 text-4xl font-serif font-bold leading-normal mb-5">
        Sign Up
      </h1>
      <Formik
        initialValues={{
          username: "",
          password: "",
          passwordConfirm: "",
          accept: false,
        }}
        validate={validate}
        onSubmit={(values, actions) => handleSubmit(values, actions, router)}
      >
        {({ errors, touched }) => (
          <Form className="flex flex-col justify-center">
            <FormField
              type="text"
              id="username"
              name="username"
              label="Username"
              error={
                errors.username && touched.username
                  ? errors.username
                  : undefined
              }
            />

            <FormField
              type="password"
              id="password"
              name="password"
              label="Password"
              error={
                errors.password && touched.password
                  ? errors.password
                  : undefined
              }
            />

            <FormField
              type="password"
              id="passwordConfirm"
              name="passwordConfirm"
              label="Confirm password"
              error={
                errors.passwordConfirm && touched.passwordConfirm
                  ? errors.passwordConfirm
                  : undefined
              }
            />

            {errors.accept && touched.accept ? (
              <div className="text-sm font-sans font-normal leading-normal text-red-500">
                {errors.accept}
              </div>
            ) : null}
            <label className="flex items-center text-gray-600 text-base font-sans font-normal leading-normal mb-7 ">
              <Field type="checkbox" name="accept" className="mr-2" />
              <span>I accept the terms and privacy policy</span>
            </label>

            <button
              type="submit"
              className="text-xl bg-[#00A870] py-4 px-8 text-white font-serif font-bold rounded mb-5"
            >
              Create an account
            </button>
          </Form>
        )}
      </Formik>
      <div className="flex justify-center">
        <p className="mr-1 text-gray-600 text-base font-sans font-normal leading-normal">
          Already have an account?
        </p>
        <Link className="text-[#6CA390]" href="/auth/login">
          Login here
        </Link>
      </div>
    </div>
  );
};

export default SignupForm;
